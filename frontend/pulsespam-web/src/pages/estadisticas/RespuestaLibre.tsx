import { Box, Button, Card, CardActions, CardContent, Typography } from "@mui/material";
import { useState } from "react";
import { Respuesta } from "../../types/Respuesta";

type RespuestaLibreProps = {
    respuesta: Respuesta;
};

function RespuestaLibre(props: RespuestaLibreProps) {
    const { respuesta } = props;

    return (
        <Box key={respuesta.id} m={2} display="flex" justifyContent="center" alignItems="center">
            <Card sx={{ maxWidth: 800 }}>
                <CardContent>
                    <Typography>{respuesta.respuestaLibre}</Typography>
                </CardContent>
            </Card>
        </Box>
    );
}

export default RespuestaLibre;