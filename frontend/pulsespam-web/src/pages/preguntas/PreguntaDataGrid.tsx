import { useState, useEffect, useCallback } from 'react'
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { DataGrid, GridActionsCellItem, GridColDef, GridColumnVisibilityModel, GridEventListener, GridRowId, GridRowParams } from "@mui/x-data-grid";
import { CATEGORIA_GET, PREGUNTA_GET, PREGUNTA_ID_DELETE, PREGUNTA_ID_GET } from '../../constants/urls';
import { Pregunta } from '../../types/Pregunta';
import { Alert, Box, Stack } from '@mui/material';
import PreguntaDialog from './PreguntaDialog';
import { PreguntaTransfer } from '../../types/PreguntaTransfer';
import PreguntaUpdDialog from './PreguntaUpdDialog';
import { tokenToString } from 'typescript';
import { token } from '../../constants/authInfo';

const PreguntaDataGrid = () => {

  const [tableData, setTableData] = useState([])

  const [preguntaEditar, setPreguntaEditar] = useState<PreguntaTransfer | null>(null);

  const [columnVisibilityModel, setColumnVisibilityModel] =
    useState<GridColumnVisibilityModel>({
      id: false,
      categoriaId: false,
      tipoId: false,
    });

  const alert = {
    showSuccessAlert: false,
    showFailAlert: false
  }
  const [state, setState] = useState(alert);

  function handleEditCancel() {
    setPreguntaEditar(null);
  }

  const handleEditClicked = (row: PreguntaTransfer) => {
    setPreguntaEditar(row);
  }

  const handleDeleteClicked = useCallback(
    (id: GridRowId) => () => {
      setTimeout(() => {
        setTableData((prevRows) => prevRows.filter((row: PreguntaTransfer) => row.id !== id));
      });
      borraPregunta(id);
    },
    [],
  );

  const columns: GridColDef[] = [
    { field: "id" },
    { field: "categoriaId" },
    { field: "tipoId" },
    { field: "preguntaTxt", headerName: "Pregunta", width: 550 },
    { field: "categoriaCat", headerName: "Categoria", width: 200 },
    { field: "tipo", headerName: "Tipo de pregunta", width: 200 },
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
    fetch(PREGUNTA_GET, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`,
      }
    })
      .then((data) => data.json())
      .then((data) => setTableData(data))

  }, [])

  function borraPregunta(id: GridRowId) {
    if (token == null) {
      throw new Error('ERROR: Token nulo');
    }
  
    return fetch(PREGUNTA_ID_DELETE(id), {
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
      {preguntaEditar && <PreguntaUpdDialog isOpen={true} onClose={handleEditCancel}
        preguntaInicial={preguntaEditar}></PreguntaUpdDialog>}
      {state.showSuccessAlert && <Alert severity={"success"}>Se ha borrado el elemento</Alert>}
      {state.showFailAlert && <Alert severity={"error"}>Se ha producido un error</Alert>}
    </Stack>
  )
}

export default PreguntaDataGrid