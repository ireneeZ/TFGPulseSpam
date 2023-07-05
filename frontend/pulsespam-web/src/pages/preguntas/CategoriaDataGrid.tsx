import { useState, useEffect, useCallback } from 'react'
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { DataGrid, GridActionsCellItem, GridColDef, GridColumnVisibilityModel, GridRowId, GridRowParams } from "@mui/x-data-grid";
import { CATEGORIA_GET, CATEGORIA_ID_DELETE, CATEGORIA_NINGUNA_ID, PREGUNTA_GET } from '../../constants/urls';
import { Pregunta } from '../../types/Pregunta';
import { Categoria } from '../../types/Categoria';
import CategoriaUpdDialog from './CategoriaUpdDialog';
import { Stack, Box, Alert } from '@mui/material';
import { token } from '../../constants/authInfo';

const CategoriaDataGrid = () => {

  const [tableData, setTableData] = useState([]);

  const [categoriaEditar, setCategoriaEditar] = useState<Categoria | null>(null);

  const alert = {
    showSuccessAlert: false,
    showFailAlert: false
  }
  const [state, setState] = useState(alert);

  const [columnVisibilityModel, setColumnVisibilityModel] =
    useState<GridColumnVisibilityModel>({
      id: false,
    });

  function handleEditCancel() {
    setCategoriaEditar(null);
  }

  const handleEditClicked = (row: Categoria) => {
    if (row.id == CATEGORIA_NINGUNA_ID) {
      return null;
    }
    setCategoriaEditar(row);
  }

  const handleDeleteClicked = useCallback(
    (id: GridRowId) => () => {
      if (id == CATEGORIA_NINGUNA_ID) {
        return null;
      }
      setTimeout(() => {
        setTableData((prevRows) => prevRows.filter((row: Categoria) => row.id !== id));
      });
      borraCategoria(id);
    },
    [],
  );


  const columns: GridColDef[] = [
    { field: "id" },
    { field: "categoriaCat", headerName: "Categoria", width: 850 },
    {
      field: "editar", width: 50, type: 'actions',
      getActions: (params: GridRowParams) => [
        <GridActionsCellItem icon={<EditIcon />} label="Editar" onClick={() => handleEditClicked(params.row)} />,
      ]
    },
    {
      field: "borrar", width: 50, type: 'actions',
      getActions: (params: GridRowParams) => [
        <GridActionsCellItem icon={<DeleteIcon />} label="Borrar" onClick={handleDeleteClicked(params.id)} />,
      ]
    }]

 useEffect(() => {
    fetch(CATEGORIA_GET, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`,
      }
    })
      .then((data) => data.json())
      .then((data) => setTableData(data))

  }, [])


  function borraCategoria(id: GridRowId) {
    return fetch(CATEGORIA_ID_DELETE(id), {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      }
    })
      .then(response => {
        if (response.status != 204) {
          throw new Error('ERROR');
        } else {
          setState({
            showSuccessAlert: true,
            showFailAlert: false
          })
        }
      })
      .catch(err => {
        setState({
          showSuccessAlert: false,
          showFailAlert: true
        })
      })
  }

  return (
    <Stack spacing={2} sx={{ width: '100%' }}>
      <Box sx={{ height: 400, width: '100%' }}>
        <DataGrid
          columnVisibilityModel={columnVisibilityModel}
          rows={tableData}
          columns={columns}
          initialState={{
            pagination: {
              paginationModel: {
                pageSize: 5,
              },
            },
          }}
          pageSizeOptions={[5]}
          disableRowSelectionOnClick />
      </Box>
      {categoriaEditar && <CategoriaUpdDialog isOpen={true} onClose={handleEditCancel}
        categoriaInicial={categoriaEditar}></CategoriaUpdDialog>}
      {state.showSuccessAlert && <Alert severity={"success"}>Se ha borrado el elemento</Alert>}
      {state.showFailAlert && <Alert severity={"error"}>Se ha producido un error</Alert>}
    </Stack>
  )
}

export default CategoriaDataGrid