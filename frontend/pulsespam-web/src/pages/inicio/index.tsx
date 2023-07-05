import { Box, Button, Grid, Link, Menu, MenuItem, Stack, Typography } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';
import { useNavigate } from "react-router-dom";

function Inicio() {
    const current = new Date();
    const date = `${current.getDate()}/${current.getMonth()+1}/${current.getFullYear()}`;

    const navigate = useNavigate();

    function handlePreguntasClicked() {
        navigate("/preguntas");
    }

    function handleRespuestasClicked() {
        navigate("/estadisticas");
    }

    function handleusuariosClicked() {
        navigate("/usuarios");
    }

    return (
        <Box display="flex" justifyContent="center" alignItems="center" minHeight="60vh" sx={{width:"95%", height: "80%"}}>
            <Stack spacing={2} alignItems={"center"}>
            <Typography variant="h4">
                Bienvenido a PulseSpam
            </Typography>
            <Typography variant="h5">
                Hoy es {date}
            </Typography>
            <Grid>
                <Button onClick={handlePreguntasClicked} startIcon={<AddIcon />}>
                    Preguntas y categorías 
                </Button>
                <Button onClick={handleRespuestasClicked} startIcon={<AddIcon />}>
                    Ver respuestas
                </Button>
                <Button onClick={handleusuariosClicked} startIcon={<AddIcon />}>
                    Usuarios 
                </Button>
            </Grid>
            </Stack>
        </Box>
    );
}

export default Inicio;