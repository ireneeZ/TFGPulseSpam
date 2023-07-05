import { Box, Button, Card, CardMedia, CircularProgress, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, MenuItem, TextField } from "@mui/material";
import { useState } from "react";
import { CATEGORIA_GET } from "../../constants/urls";
import { token } from "../../constants/authInfo";

type CategoriaDialogProps = {
  isOpen: boolean;
  categoriaTxt: string;
  onClose: (value?: string) => void;
};

function CategoriaDialog(props: CategoriaDialogProps) {
  const { isOpen, onClose, categoriaTxt: categoriaProp } = props;

  const [categoriaTxt, setCategoriaTxt] = useState(categoriaProp);
  const [errorCategoriaTxt, setErrorCategoriaTxt] = useState(false);

  const handleCancel = () => {
    onClose();
    setCategoriaTxt("");
  };

  const handleOk = () => {
    if (categoriaTxt != "") {
      onClose(categoriaTxt);
      creaCategoria(categoriaTxt);
      setCategoriaTxt("");
    } else {
      setErrorCategoriaTxt(true);
    }
  };

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setCategoriaTxt((event.target as HTMLInputElement).value);
    if (errorCategoriaTxt) {
      setErrorCategoriaTxt(false);
    }
  };

  function creaCategoria(categoriaTxt: string) {
    return fetch(CATEGORIA_GET, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
      body: JSON.stringify({ 
        'CategoriaCat': categoriaTxt 
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

  return (<Dialog open={isOpen}
    maxWidth="sm" fullWidth={true}
  >
    <DialogTitle> Crear categoría </DialogTitle>
    <DialogContent sx={{ p: 2 }}>
      <DialogContentText>
        Introduce el nombre de la nueva categoría.
      </DialogContentText>
      <TextField
        autoFocus
        margin="normal"
        label="Categoría"
        type="text"
        fullWidth
        variant="standard"
        value={categoriaTxt}
        onChange={handleChange}
        helperText={errorCategoriaTxt ? "Es necesario introducir una categoría" : ""}
        error={errorCategoriaTxt}
      />
    </DialogContent>
    <DialogActions>
      <Button onClick={handleCancel}> Cerrar </Button>
      <Button onClick={handleOk}> Crear </Button>
    </DialogActions>
  </Dialog>);
}

export default CategoriaDialog;