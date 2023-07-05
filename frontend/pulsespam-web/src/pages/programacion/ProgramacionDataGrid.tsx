import { useState, useEffect, useCallback } from 'react'
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { DataGrid, GridActionsCellItem, GridColDef, GridColumnVisibilityModel, GridRenderCellParams, GridRowId, GridRowParams } from "@mui/x-data-grid";
import { CATEGORIA_GET, PREGUNTA_GET, PROGRAMACION_GET, TAREA_PROGRAMACION_DELETE, TAREA_PROGRAMACION_GET } from '../../constants/urls';
import { Pregunta } from '../../types/Pregunta';
import { Alert, Box, List, Stack } from '@mui/material';
import { TareaProgramacion } from '../../types/TareaProgramacion';
import { TareaProgramacionTransfer } from '../../types/TareaProgramacionTransfer';
import PreguntaUpdDialog from '../preguntas/PreguntaUpdDialog';
import { token } from '../../constants/authInfo';

const ProgramacionDataGrid = () => {

  const [tableData, setTableData] = useState([]);

  const alert = {
    showSuccessAlert: false,
    showFailAlert: false
  }
  const [state, setState] = useState(alert);

  const handleDeleteClicked = useCallback(
    (id: GridRowId) => () => {
      setTimeout(() => {
        setTableData((prevRows) => prevRows.filter((row: TareaProgramacionTransfer) => row.id !== id));
      });
      borraTarea(id);
    },
    [],
  );

  const [columnVisibilityModel, setColumnVisibilityModel] =
    useState<GridColumnVisibilityModel>({
      id: false
    });

  const columns: GridColDef[] = [
    { field: "id", headerName: "TareaId" },
    {
      field: "preguntas", headerName: "Preguntas", width: 550,
      renderCell: (params) => (
        <ul className="flex">
          {params.value.map((pregunta: Pregunta) => (
            <li>{pregunta.preguntaTxt}</li>
          ))}
        </ul>
      ),
    },
    {
      field: "fechaInicio", headerName: "Fecha de inicio", width: 150, type: "date",
      valueFormatter: params => new Date(params?.value).toDateString().toLocaleString(),
    },
    {
      field: "fechaFin", headerName: "Fecha de fin", width: 150, type: "date",
      valueFormatter: params => new Date(params?.value).toDateString().toLocaleString(),
    },
    {
      field: "hora", headerName: "Hora", width: 150,
      valueFormatter: params => new Date(params?.value).toLocaleTimeString()
    },
    {
      field: "borrar", width: 50, type: 'actions',
      getActions: (params: GridRowParams) => [
        <GridActionsCellItem icon={<DeleteIcon />} label="Borrar" onClick={handleDeleteClicked(params.id)}/>,
      ]
    }]


    useEffect(() => {
      fetch(TAREA_PROGRAMACION_GET, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`,
        }
      })
        .then((data) => data.json())
        .then((data) => setTableData(data))
  
    }, [])

  function borraTarea(id: GridRowId) {
    return fetch(TAREA_PROGRAMACION_DELETE(id), {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`,
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
          getRowHeight={() => 'auto'}
          initialState={{
            pagination: {
              paginationModel: {
                pageSize: 5,
              },
            },
          }}
          pageSizeOptions={[5]}
          disableRowSelectionOnClick 
          />
      </Box>
      {state.showSuccessAlert && <Alert severity={"success"}>Programación borrada</Alert>}
      {state.showFailAlert && <Alert severity={"error"}>Se ha producido un error</Alert>}
    </Stack>
  )
}

export default ProgramacionDataGrid