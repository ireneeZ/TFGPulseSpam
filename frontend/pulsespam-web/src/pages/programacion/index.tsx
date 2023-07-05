import { Alert, Box, Button, Card, Checkbox, Container, FormControlLabel, FormGroup, Grid, Paper, Tab, Tabs, Typography } from "@mui/material";
import CheckboxPreguntas, { preguntasCheckedExport } from "./CheckboxPreguntas";
import { tab } from "@testing-library/user-event/dist/tab";
import TabPanel from "../../components/TabPanel";
import React, { useState } from "react";
import AddIcon from '@mui/icons-material/Add';
import ProgramacionDataGrid from "./ProgramacionDataGrid";
import { DateCalendar, DatePicker, LocalizationProvider, TimePicker } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import dayjs, { Dayjs } from "dayjs";
import { TareaProgramacion } from "../../types/TareaProgramacion";
import { TAREA_PROGRAMACION_GET } from "../../constants/urls";
import { token } from "../../constants/authInfo";

const today = dayjs();

const isWeekend = (date: Dayjs) => {
    const day = date.day();
    return day === 0 || day === 6;
};

function Programacion() {
    const [tab, setTab] = useState(0);

    const [fechaInicio, setFechaInicio] = useState<Dayjs | null>(today);
    const [fechaFin, setFechaFin] = useState<Dayjs | null>();
    const [hora, setHora] = useState<Dayjs | null>(today);

    const [preguntasChecked, setPreguntasChecked] = useState<string[]>([]);

    const [errorAlertText, setErrorAlertText] = useState<string>("Se ha producido un error");

    const alert = {
        showSuccessAlert: false,
        showFailAlert: false
    }
    const [state, setState] = useState(alert);

    const handleTabChange = (event: React.SyntheticEvent, newValue: number) => {
        setTab(newValue);
    };

    function handleHoraChange(nuevaHora: Dayjs | null) {
        setHora(nuevaHora);
    }

    function handleFechaInicioChange(nuevaFecha: Dayjs | null) {
        setFechaInicio(nuevaFecha);
    }

    function handleFechaFinChange(nuevaFecha: Dayjs | null) {
        setFechaFin(nuevaFecha);
    }

    const handleProgramacionOk = () => {
        if (fechaInicio != null && fechaFin != null && hora != null && preguntasCheckedExport.length != 0) {
            var fechaIniDate = fechaInicio.toDate();
            var fechaFinDate = fechaFin.toDate();
            var horaString = hora.toString();
            const programacion = {
                fechaIni: fechaIniDate, fechaFin: fechaFinDate, hora: horaString,
                preguntasId: preguntasCheckedExport
            } as TareaProgramacion;
            creaProgramacion(programacion);
            //Limpiar preguntas
            setPreguntasChecked([]);
        } else {
            setErrorAlertText("Es necesario completar todos los campos");
            setState({
                showFailAlert: true,
                showSuccessAlert: false
              })
        }
    };

    function creaProgramacion(tarea: TareaProgramacion) {    
        return fetch(TAREA_PROGRAMACION_GET, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`,
            },
            body: JSON.stringify({
                'FechaInicio': tarea.fechaIni,
                'FechaFin': tarea.fechaFin,
                'Hora': tarea.hora,
                'PreguntasId': tarea.preguntasId
            })
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('ERROR');
                } else {
                    setState({
                        showFailAlert: false,
                        showSuccessAlert: true
                      })
                    return response.json();
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
        <Box display="left" justifyContent="space-between" sx={{ p: 2 }}>
            <Tabs value={tab} onChange={handleTabChange}>
                <Tab label="Nueva programación" />
                <Tab label="Ver programación" />
            </Tabs>

            <TabPanel value={tab} index={0}>
                <Grid container spacing={2}>
                    <Grid item xs={8}>
                        <Paper style={{ maxHeight: 350, overflow: 'auto' }}>
                            <CheckboxPreguntas preguntasChecked={preguntasChecked} ></CheckboxPreguntas>
                        </Paper>
                    </Grid>
                    <Grid item xs={8}>
                        <Grid container spacing={3}>
                            <Grid item xs={3}>
                                <Typography variant="h6"> Fecha de inicio </Typography>
                                <DatePicker
                                    defaultValue={today}
                                    disablePast
                                    views={['year', 'month', 'day']}
                                    shouldDisableDate={isWeekend}
                                    value={fechaInicio}
                                    onChange={handleFechaInicioChange}
                                />
                            </Grid>
                            <Grid item xs={3}>
                                <Typography variant="h6"> Fecha de fin </Typography>
                                <DatePicker
                                    disablePast
                                    views={['year', 'month', 'day']}
                                    shouldDisableDate={isWeekend}
                                    value={fechaFin}
                                    onChange={handleFechaFinChange}
                                />
                            </Grid>
                            <Grid item xs={3}>
                                <Typography variant="h6"> Hora </Typography>
                                <TimePicker
                                    defaultValue={today}
                                    value={hora}
                                    onChange={handleHoraChange}
                                />
                            </Grid>
                        </Grid>
                    </Grid>
                    <Grid item xs={8}>
                        <Button startIcon={<AddIcon />} onClick={handleProgramacionOk}>Programar</Button>
                    </Grid>
                </Grid>
 
                { state.showSuccessAlert && <Alert severity={"success"}>Programación creada</Alert>}

                { state.showFailAlert && <Alert severity={"error"}>{errorAlertText}</Alert>}

            </TabPanel>
            <TabPanel value={tab} index={1}>
                <Box sx={{ height: 400, width: '95%' }} >
                    <ProgramacionDataGrid></ProgramacionDataGrid>
                </Box>
            </TabPanel>
        </Box>
    );
}

export default Programacion;