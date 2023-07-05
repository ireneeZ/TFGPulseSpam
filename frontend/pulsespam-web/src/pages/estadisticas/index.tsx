import { Alert, Box, Button, Container, Grid, MenuItem, Stack, TextField, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { token } from "../../constants/authInfo";
import { PREGUNTA_GET, PREGUNTA_ID_GET, SCHED_FECHA_GET } from "../../constants/urls";
import { Pregunta } from "../../types/Pregunta";
import { Dayjs } from "dayjs";
import { DatePicker } from "@mui/x-date-pickers";
import { Respuesta } from "../../types/Respuesta";
import { ScheduleTransfer } from "../../types/ScheduleTransfer";
import { PreguntaTransfer } from "../../types/PreguntaTransfer";
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
import { Pie } from 'react-chartjs-2';
import RespuestaLibre from "./RespuestaLibre";
import PieChart from "./PieChart";

function Estadisticas() {
    const isWeekend = (date: Dayjs) => {
        const day = date.day();
        return day === 0 || day === 6;
    };

    const [alertState, setAlertState] = useState(false);
    const [alertText, setAlertText] = useState("");

    const [fecha, setFecha] = useState<Dayjs | null>();

    const [schedule, setSchedule] = useState<ScheduleTransfer | null>();
    const [pregunta, setPregunta] = useState<PreguntaTransfer>();

    const [respuestaLibre, setRespuestaLibre] = useState(false);
    const [respuestaPuntuacion, setRespuestaPuntuacion] = useState(false);
    const [respuestaSiNo, setRespuestaSiNo] = useState(false);

    const [respuestaPuntuacionMedia, setRespuestaPuntuacionMedia] = useState<number>();
    const [respuestaLibreLista, setRespuestaLibreLista] = useState<Respuesta[]>();
    const [numRespuestas, setNumRespuestas] = useState<number>(0);
    const [numRespuestasSi, setNumRespuestasSi] = useState<number>(0);
    const [numRespuestasNo, setNumRespuestasNo] = useState<number>(0);

    const [pieChartData, setPieChartData] = useState<number[]>([]);

    function handleFechaChange(nuevaFecha: Dayjs | null) {
        if (nuevaFecha != null) {
            setFecha(nuevaFecha)
        }
    }

    const handleBuscarClicked = () => {
        if (fecha != null) {
            const date = fecha.toDate().toString().substring(0, 10);
            getSchedule(date);
        } else {
            setAlertState(true);
            setAlertText("Es necesario introducir una fecha");
        }
    }

    function getSchedule(fecha: string) {
        return (fetch(SCHED_FECHA_GET(fecha), {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
        })
            .then(response => {
                if (response.ok) {
                    setAlertState(false);
                    response.json().then((s: ScheduleTransfer) => {
                        setSchedule(s);
                        getPregunta(s.pregunta.id);

                        var respuestas = s.respuestas;

                        if (typeof respuestas !== 'undefined' && respuestas.length > 0) {
                            var tipo = respuestas[0].tipo;
                            switch (tipo) {
                                case "Puntuacion":
                                    handleRespuestaPuntuacion(respuestas);
                                    break;
                                case "Libre":
                                    handleRespuestaLibre(respuestas);
                                    break;
                                case "SiNo":
                                    handleRespuestaSiNo(respuestas);
                                    break;
                                default:
                                    break;
                            }
                        } else {
                            setSchedule(null);
                            setAlertText("Todavía no se han recibido respuestas");
                            setAlertState(true);
                            setRespuestaLibre(false);
                            setRespuestaPuntuacion(false);
                            setRespuestaSiNo(false);
                        }
                    });
                } else if (response.status == 404) {
                    setSchedule(null);
                    setAlertText("No existe una pregunta asignada para el día indicado");
                    setAlertState(true);
                    setRespuestaLibre(false);
                    setRespuestaPuntuacion(false);
                    setRespuestaSiNo(false);
                } else {
                    throw new Error('ERROR');
                }
            })
            .catch(err => {
                setAlertText("Se ha producido un error");
                setAlertState(true);
                setSchedule(null);
                setRespuestaLibre(false);
                setRespuestaPuntuacion(false);
                setRespuestaSiNo(false);
            }))
    }

    function getPregunta(id: string) {
        return (fetch(PREGUNTA_ID_GET(id), {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('ERROR');
                } else {
                    response.json().then((p: PreguntaTransfer) => {
                        setPregunta(p);
                    });
                }
            })
            .catch(err => {
                setAlertText("Se ha producido un error");
                setAlertState(true);
            }))
    }

    function handleRespuestaLibre(respuestas: Respuesta[]) {
        setRespuestaLibre(true);
        setRespuestaPuntuacion(false);
        setRespuestaSiNo(false);
        setRespuestaLibreLista(respuestas);
    }

    function handleRespuestaSiNo(respuestas: Respuesta[]) {
        setRespuestaLibre(false);
        setRespuestaPuntuacion(false);
        setRespuestaSiNo(true);

        var si = 0;
        var no = 0;
        var puntuacion:number[] = [0,0];
        var num = 0;
        respuestas.forEach(r => {
            if (r.respuestaSiNo) {
                si++;
            } else {
                no++;
            }
            num++;
        });

        puntuacion[0] = si;
        puntuacion[1] = no;

        setNumRespuestas(num);
        setNumRespuestasSi(si);
        setNumRespuestasNo(no);
        setPieChartData(puntuacion);
    }

    function handleRespuestaPuntuacion(respuestas: Respuesta[]) {
        setRespuestaLibre(false);
        setRespuestaPuntuacion(true);
        setRespuestaSiNo(false);

        var counter = 0;
        var num = 0;

        var puntuacion:number[] = [0,0,0,0,0];

        respuestas.forEach(r => {
            counter += r.respuestaPuntuacion;
            num++;
            puntuacion[r.respuestaPuntuacion-1]++
        });
        var media = counter / respuestas.length;

        setRespuestaPuntuacionMedia(media);
        setNumRespuestas(num);
        setPieChartData(puntuacion);
    }

    return (
        <Stack spacing={4} sx={{ width: "95%", height: "80%" }} alignContent="center" alignItems="center">
            <Typography variant="h4">
                Estadisticas
            </Typography>
            <Grid>
                <Grid>
                    <DatePicker
                        views={['year', 'month', 'day']}
                        shouldDisableDate={isWeekend}
                        value={fecha}
                        onChange={handleFechaChange}
                    />
                </Grid>
                <Grid>
                    <Button onClick={handleBuscarClicked}>Buscar</Button>
                </Grid>
            </Grid>
            {schedule && <Stack spacing={0} sx={{ width: "95%", height: "80%" }}>
                <Typography variant="h5">Pregunta del día: {pregunta?.preguntaTxt}</Typography>
                <br></br>
                <Typography variant="h6">
                    Información de la pregunta
                </Typography>
                <Typography>Categoría: {pregunta?.categoriaCat}</Typography>
                <Typography>Tipo de pregunta: {pregunta?.tipo}</Typography>
            </Stack>}
            {alertState && <Alert severity={"error"}>{alertText}</Alert>}

            {respuestaPuntuacion && <Stack sx={{ width: "95%", height: "80%" }}>
                <Typography variant="h6">
                    Puntuaciones
                </Typography>
                <Typography>Número total de respuestas: {numRespuestas}</Typography>
                <Typography>Puntuación media {respuestaPuntuacionMedia}</Typography>
                <Stack direction="row" spacing={2} paddingBottom="5%">
                    <PieChart labels={["1","2","3","4","5"]} label={"Respuestas"} 
                        data={pieChartData} height="400" width="400"></PieChart>
                </Stack>
            </Stack>}
            {respuestaLibre && <Stack spacing={0} sx={{ width: "95%", height: "80%" }}>
                <Typography variant="h6">
                    Respuestas obtenidas
                </Typography>
                {respuestaLibreLista && respuestaLibreLista.map((respuesta) => (
                    <RespuestaLibre respuesta={respuesta}></RespuestaLibre>
                ))}
            </Stack>}
            {respuestaSiNo && <Stack spacing={0} sx={{ width: "95%", height: "80%" }}>
                <Typography variant="h6">
                    Respuestas obtenidas
                </Typography>
                <Typography>Número total de respuestas: {numRespuestas}</Typography>
                <Typography>Sí: {numRespuestasSi}</Typography>
                <Typography>No: {numRespuestasNo}</Typography>
                <Stack direction="row" spacing={2} height="50" width="50">
                    <PieChart labels={["Si","No"]} label={"Respuestas"} 
                        data={pieChartData} height="400" width="400"></PieChart>
                </Stack>
            </Stack>}
        </Stack>
    );
}

export default Estadisticas;