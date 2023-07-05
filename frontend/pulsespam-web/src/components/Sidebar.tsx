import { ProSidebar, Menu, MenuItem, SidebarHeader, SidebarContent, SidebarFooter } from "react-pro-sidebar";
import { createTheme, colors, ThemeProvider, ListItemIcon, ListItemText } from "@mui/material";
import 'react-pro-sidebar/dist/css/styles.css';
import { Box, IconButton, Typography, useTheme } from "@mui/material";
import { Link, Navigate, useNavigate } from "react-router-dom";
import HomeOutlinedIcon from "@mui/icons-material/HomeOutlined";
import QuestionMarkIcon from '@mui/icons-material/QuestionMark';
import ShowChartIcon from '@mui/icons-material/ShowChart';
import PersonIcon from '@mui/icons-material/Person';
import DateRangeIcon from '@mui/icons-material/DateRange';

function Sidebar() {
    const navigate = useNavigate();
    
    function logout() {
        
        localStorage.removeItem("user");
        navigate("/login");
    }

    return (
        <ProSidebar style={{ height: "100vh", position: "fixed"}}>
           <SidebarHeader style={{ textAlign: 'left' }}>
                <div
                    style={{
                        padding: '9px',
                        textTransform: 'uppercase',
                        fontWeight: 'bold',
                        fontSize: 32,
                        letterSpacing: '1px'
                    }}
                >
                Pulsespam
                </div>
            </SidebarHeader> 
            {/* Menu para la barra */}
            <SidebarContent>
            <Menu>
                <MenuItem icon={<HomeOutlinedIcon/>}>
                    Inicio <Link to="/inicio" />
                </MenuItem>
                <MenuItem icon={<QuestionMarkIcon/>}>
                    Preguntas <Link to="/preguntas" />
                </MenuItem>
                <MenuItem icon={<DateRangeIcon/>}>
                    Programación <Link to="/programacion" />
                </MenuItem>
                <MenuItem icon={<ShowChartIcon/>}>
                    Estadísticas <Link to="/estadisticas" />
                </MenuItem>
                <MenuItem icon={<PersonIcon/>}>
                    Usuarios <Link to="/usuarios" />
                </MenuItem>
            </Menu>
            </SidebarContent>
            <SidebarFooter style={{ textAlign: 'center' }}>
                <Menu>
                    <MenuItem icon={<PersonIcon/>} onClick={logout}>Cerrar sesión</MenuItem>
                </Menu>
            </SidebarFooter>
        </ProSidebar>
    );
}

export default Sidebar;