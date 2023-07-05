import { useState, useEffect, useCallback } from 'react'
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { DataGrid, GridActionsCellItem, GridColDef, GridColumnVisibilityModel, GridRenderCellParams, GridRowId, GridRowParams } from "@mui/x-data-grid";
import { CATEGORIA_GET, PREGUNTA_GET, PROGRAMACION_GET, TAREA_PROGRAMACION_DELETE, TAREA_PROGRAMACION_GET, USUARIOS_GET, USUARIOS_ID_DELETE, USUARIO_DELETE } from '../../constants/urls';
import { Pregunta } from '../../types/Pregunta';
import { Alert, Box, List, Stack } from '@mui/material';
import { TareaProgramacion } from '../../types/TareaProgramacion';
import { TareaProgramacionTransfer } from '../../types/TareaProgramacionTransfer';
import PreguntaUpdDialog from '../preguntas/PreguntaUpdDialog';
import { Usuario } from '../../types/Usuario';
import { token } from '../../constants/authInfo';

const UsuariosDataGrid = () => {

  const [tableData, setTableData] = useState([]);

  const alert = {
    showSuccessAlert: false,
    showFailAlert: false
  }
  const [state, setState] = useState(alert);

  const handleDeleteClicked = useCallback(
    (id: GridRowId) => () => {
      setTimeout(() => {
        setTableData((prevRows) => prevRows.filter((row: Usuario) => row.id !== id));
      });
      borraUsuario(id);
    },
    [],
  );

  const [columnVisibilityModel, setColumnVisibilityModel] =
    useState<GridColumnVisibilityModel>({
      id: false,
      password: false,
    });

  const columns: GridColDef[] = [
    { field: "id", headerName: "Id" },
    {
      field: "userName", headerName: "Nombre de usuario", width: 400 
    },
    {
      field: "email", headerName: "Email", width: 500 
    },
    {
      field: "borrar", width: 50, type: 'actions',
      getActions: (params: GridRowParams) => [
        <GridActionsCellItem icon={<DeleteIcon />} label="Borrar" onClick={handleDeleteClicked(params.id)}/>,
      ]
    }]


  useEffect(() => {
    fetch(USUARIOS_GET, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      }
    })
      .then((data) => data.json())
      .then((data) => setTableData(data))

  }, [])

  function borraUsuario(id: GridRowId) {
    return fetch(USUARIOS_ID_DELETE(id), {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      }
    })
      .then(response => {
        if (!response.ok) {
          throw new Error('ERROR');
        } else {
          setState({
            showFailAlert: false,
            showSuccessAlert: true
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
          pageSizeOptions={[5]}
          disableRowSelectionOnClick 
          />
      </Box>
      {state.showSuccessAlert && <Alert severity={"success"}>Usuario borrado</Alert>}
      {state.showFailAlert && <Alert severity={"error"}>Se ha producido un error</Alert>}
    </Stack>
  )
}

export default UsuariosDataGrid