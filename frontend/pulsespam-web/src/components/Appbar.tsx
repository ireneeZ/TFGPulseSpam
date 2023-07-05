
import { Box, IconButton, InputBase } from '@mui/material';
import { createTheme, colors, ThemeProvider } from "@mui/material";

import SearchIcon from "@mui/icons-material/Search";
import AccountCircleIcon from '@mui/icons-material/AccountCircle';

function Appbar() {
    return (
        <Box display="flex" justifyContent="space-between" p={2}>
          {/* Barra de busqueda */}
          <Box
            display="flex">
            <InputBase sx={{ ml: 2, flex: 1 }} placeholder="Buscar" />
            <IconButton type="button">
              <SearchIcon />
            </IconButton>
          </Box>
    
          {/* Iconos */}
          <Box display="flex">
            <IconButton>
              <AccountCircleIcon />
            </IconButton>
          </Box>
        </Box>
      );
}

export default Appbar;