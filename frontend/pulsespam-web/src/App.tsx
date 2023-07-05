import React from "react";
import { createTheme, colors, ThemeProvider, Grid } from "@mui/material";

import Appbar from "./components/Appbar";
import Sidebar from "./components/Sidebar";

import { Routes, Route, Navigate } from "react-router-dom";
import Inicio from "./pages/inicio";
import Preguntas from "./pages/preguntas";
import Usuarios from "./pages/usuarios";
import Programacion from "./pages/programacion";
import Estadisticas from "./pages/estadisticas";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import Login from "./pages/auth/Login";
import LayoutSistema from "./components/LayoutSistema";
import PrivateRoutes from "./components/PrivateRoutes";

function App() {
  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <div className="app">
        <main className="main">
              <Routes>
              <Route element={<PrivateRoutes/>}>
                <Route element={<LayoutSistema />}>
                  <Route path="/inicio" element={<Inicio />} />
                  <Route path="/preguntas" element={<Preguntas />} />
                  <Route path="/programacion" element={<Programacion />} />
                  <Route path="/estadisticas" element={<Estadisticas />} />
                  <Route path="/usuarios" element={<Usuarios />} />
                  <Route path="*" element={<Navigate to="/login" replace />} />
                </Route>
              </Route>
              <Route path="/login" element={<Login />} />             
              </Routes>
        </main>
      </div>
    </LocalizationProvider>
  );
}


export default App;
