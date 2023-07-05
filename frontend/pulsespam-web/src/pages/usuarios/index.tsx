import { Box, Button, Tab, Tabs, Typography } from "@mui/material";
import UsuariosDataGrid from "./UsuariosDataGrid";
import { useState } from "react";
import TabPanel from "../../components/TabPanel";
import AddIcon from '@mui/icons-material/Add';
import UsuarioDialog from "./UsuarioDialog";

function Usuarios() {
    const [tab, setTab] = useState(0);

    const [creaUsuarioOpen, setCreaUsuarioOpen] = useState(false);

    const handleTabChange = (event: React.SyntheticEvent, newValue: number) => {
        setTab(newValue);
    };

    function handleUsuarioOk() {
        setCreaUsuarioOpen(true);
    }

    function handleUsuarioCancel() {
        setCreaUsuarioOpen(false);
    }

    return (
    <Box display="left" justifyContent="space-between" sx={{ p: 2 }}>
            <Tabs value={tab} onChange={handleTabChange}>
                <Tab label="Ver usuarios" />
            </Tabs>
            <TabPanel value={tab} index={0}>
            <Box sx={{ height: 400, width: '95%' }} >
                <Box display="flex" justifyContent="space-between" sx={{ p: 2 }}>
                    <Button startIcon={<AddIcon />} onClick={handleUsuarioOk}>Añadir usuario</Button>
                    <UsuarioDialog isOpen={creaUsuarioOpen} onClose={handleUsuarioCancel} />
                </Box>
                <UsuariosDataGrid></UsuariosDataGrid>
            </Box>
            </TabPanel>
        </Box>
    );
}

export default Usuarios;