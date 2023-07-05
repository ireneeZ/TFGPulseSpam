import { Box, Button, Card, CardMedia, CircularProgress, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, MenuItem, TextField } from "@mui/material";
import { useEffect, useState } from "react";
import { Categoria } from "../../types/Categoria";
import { CATEGORIA_GET, CATEGORIA_ID_GET, PREGUNTA_GET, PREGUNTA_ID_GET, TIPO_PREGUNTA_GET } from "../../constants/urls";
import { Pregunta } from "../../types/Pregunta";
import { TipoPregunta } from "../../types/TipoPregunta";
import { DataGrid, GridEventListener } from "@mui/x-data-grid";
import { PreguntaTransfer } from "../../types/PreguntaTransfer";
import { token } from "../../constants/authInfo";

type PreguntaDialogProps = {
  isOpen: boolean;
  categoriaInicial: Categoria;
  onClose: (value?: string) => void;
};

function CategoriaUpdDialog(props: PreguntaDialogProps) {
  const { isOpen, onClose, categoriaInicial } = props;

  const [CategoriaTxt, setCategoriaTxt] = useState(categoriaInicial.categoriaCat);
  
  const handleCancel = () => {
    onClose();
  };

  const handleOk = () => {
    onClose();
    const categoria = { categoriaCat: CategoriaTxt, id: categoriaInicial.id} as Categoria; 
    updCategoria(props.categoriaInicial.id, categoria);
  };

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setCategoriaTxt((event.target as HTMLInputElement).value);
  };

  function updCategoria(idCategoria:string, categoria: Categoria) {
    return fetch(CATEGORIA_ID_GET(idCategoria), {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
      body: JSON.stringify({ 
        'categoriaCat': categoria.categoriaCat
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
    <DialogTitle> Actualizar categoria </DialogTitle>
    <DialogContent sx={{ p: 2 }}>
      <DialogContentText>
        Introduce el nuevo nombre de la categoria.
      </DialogContentText>
      <TextField
        autoFocus
        margin="normal"
        label="Categoría"
        type="text"
        fullWidth
        variant="standard"
        value={CategoriaTxt}
        onChange={handleChange}
      />
    </DialogContent>
    <DialogActions>
      <Button onClick={handleCancel}> Cerrar </Button>
      <Button onClick={handleOk}> Actualizar </Button>
    </DialogActions>
  </Dialog>);
}

export default CategoriaUpdDialog;