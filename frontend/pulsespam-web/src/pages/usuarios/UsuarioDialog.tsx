import { Box, Button, Card, CardMedia, CircularProgress, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, FormControlLabel, MenuItem, Switch, TextField } from "@mui/material";
import { useEffect, useState } from "react";
import { Categoria } from "../../types/Categoria";
import { CATEGORIA_GET, DEPARTAMENTOS_GET, PREGUNTA_GET, REGISTRO, TIPO_PREGUNTA_GET, USUARIOS_GET } from "../../constants/urls";
import { Pregunta } from "../../types/Pregunta";
import { TipoPregunta } from "../../types/TipoPregunta";
import { DataGrid, GridEventListener } from "@mui/x-data-grid";
import { PreguntaTransfer } from "../../types/PreguntaTransfer";
import { Usuario } from "../../types/Usuario";
import { Departamento } from "../../types/Departamento";
import { UsuarioRegistro } from "../../types/UsuarioRegistro";
import { token } from "../../constants/authInfo";

type PreguntaDialogProps = {
  isOpen: boolean;
  onClose: (value?: string) => void;
};

function UsuarioDialog(props: PreguntaDialogProps) {
  const { isOpen, onClose } = props;

  const [usuario, setUsuario] = useState<string>();
  const [password, setPassword] = useState<string>();
  const [email, setEmail] = useState<string>();
  const [isAdmin, setIsAdmin] = useState<boolean>(false);

  const handleCancel = () => {
    onClose();
  };

  const handleOk = () => {
    onClose();
    const user = { UserName: usuario, Password: password, Email: email, IsAdmin: isAdmin } as UsuarioRegistro;
    creaUsuario(user);
  };

  const handleUsuarioChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setUsuario((event.target as HTMLInputElement).value);
  };

  const handlePassChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPassword((event.target as HTMLInputElement).value);
  };

  const handleEmailChange = (event: React.ChangeEvent<HTMLElement>) => {
    setEmail((event.target as HTMLInputElement).value);
  };

  const handleAdminChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setIsAdmin(event.target.checked);
  };

  return (<Dialog open={isOpen}
    maxWidth="sm" fullWidth={true}
  >
    <DialogTitle> Crear usuario </DialogTitle>
    <DialogContent sx={{ p: 2 }}>
      <DialogContentText>
        Introduce los datos para crear un nuevo usuario.
      </DialogContentText>
      <TextField
        autoFocus
        margin="normal"
        label="Usuario"
        type="text"
        fullWidth
        variant="standard"
        value={usuario}
        onChange={handleUsuarioChange}
        defaultValue=""
      />
      <TextField
        autoFocus
        margin="normal"
        label="Contraseña"
        type="text"
        fullWidth
        variant="standard"
        value={password}
        onChange={handlePassChange}
        defaultValue=""
      />
      <TextField
        autoFocus
        margin="normal"
        label="Email"
        type="text"
        fullWidth
        variant="standard"
        value={email}
        onChange={handleEmailChange}
        defaultValue=""
      />
      <FormControlLabel
          control={
            <Switch checked={isAdmin} onChange={handleAdminChange} name="switch-admin-change" />
          }
          label="Permisos de administrador"
        />
    </DialogContent>
    <DialogActions>
      <Button onClick={handleCancel}> Cerrar </Button>
      <Button onClick={handleOk}> Crear </Button>
    </DialogActions>
  </Dialog>);
}

function creaUsuario(usuario: UsuarioRegistro) {
  return fetch(REGISTRO, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify({
      'UserName': usuario.UserName,
      'Password': usuario.Password,
      'Email': usuario.Email,
      'IsAdmin': usuario.IsAdmin
    })
  })
  .then(response => {
    if (!response.ok) {
      throw new Error('ERROR');
    } else {
      return response.json();
    }
  })
  .catch(err => {
  })
}

export default UsuarioDialog;