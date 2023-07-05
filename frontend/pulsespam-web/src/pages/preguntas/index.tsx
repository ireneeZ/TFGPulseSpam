import { Box, Button, Typography, useTheme } from "@mui/material";
import { DataGrid, GridActionsCellItem, GridColDef, GridRowParams } from "@mui/x-data-grid";
import AddIcon from '@mui/icons-material/Add';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import PreguntaDialog from "./PreguntaDialog";
import { useState } from "react";
import PreguntaDataGrid from "./PreguntaDataGrid";
import CategoriaDialog from "./CategoriaDialog";
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import React from "react";
import CategoriaDataGrid from "./CategoriaDataGrid";
import TabPanel from "../../components/TabPanel";

export default function Preguntas() {
    const [preguntaOpen, setPreguntaOpen] = useState(false);
    const [categoriaOpen, setCategoriaOpen] = useState(false);

    const [tab, setTab] = React.useState(0);

    const handleTabChange = (event: React.SyntheticEvent, newValue: number) => {
        setTab(newValue);
    };


    function handlePreguntaOk() {
        setPreguntaOpen(true);
    }

    function handlePreguntaCancel() {
        setPreguntaOpen(false);
    }

    function handleCategoriaOk() {
        setCategoriaOpen(true);
    }

    function handleCategoriaCancel() {
        setCategoriaOpen(false);
    }

    return (
        <div>
        <Tabs value={tab} onChange={handleTabChange} aria-label="icon tabs example">
            <Tab label="Preguntas"/>
            <Tab label="Categorías"/>
        </Tabs>

        <TabPanel value={tab} index={0}>
            <Box sx={{ height: 400, width: '95%' }} >
                <Box display="flex" justifyContent="space-between" sx={{ p: 2 }}>
                    <Button startIcon={<AddIcon />} onClick={handlePreguntaOk}>Crear pregunta</Button>
                    <PreguntaDialog isOpen={preguntaOpen} onClose={handlePreguntaCancel} preguntaTxt={""} 
                    categoriaCat={""} tipo={""}/>
                </Box>
                <PreguntaDataGrid></PreguntaDataGrid>
            </Box>
        </TabPanel>
            
        <TabPanel value={tab} index={1}>
        <Box sx={{ height: 400, width: '95%' }} >
                <Box display="flex" justifyContent="space-between" sx={{ p: 2 }}>
                    <Button startIcon={<AddIcon />} onClick={handleCategoriaOk}>Crear categoría</Button>
                    <CategoriaDialog isOpen={categoriaOpen} onClose={handleCategoriaCancel} categoriaTxt={""} />
                </Box>
                <CategoriaDataGrid></CategoriaDataGrid>
            </Box>
        </TabPanel>    
        </div>
    );
}