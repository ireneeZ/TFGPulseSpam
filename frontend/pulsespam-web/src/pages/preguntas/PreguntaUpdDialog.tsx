import { Box, Button, Card, CardMedia, CircularProgress, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, MenuItem, TextField } from "@mui/material";
import { useEffect, useState } from "react";
import { Categoria } from "../../types/Categoria";
import { CATEGORIA_GET, PREGUNTA_GET, PREGUNTA_ID_GET, TIPO_PREGUNTA_GET } from "../../constants/urls";
import { Pregunta } from "../../types/Pregunta";
import { TipoPregunta } from "../../types/TipoPregunta";
import { DataGrid, GridEventListener } from "@mui/x-data-grid";
import { PreguntaTransfer } from "../../types/PreguntaTransfer";
import { token } from "../../constants/authInfo";

type PreguntaDialogProps = {
  isOpen: boolean;
  preguntaInicial: PreguntaTransfer;
  onClose: (value?: string) => void;
};

function PreguntaUpdDialog(props: PreguntaDialogProps) {
  const { isOpen, onClose, preguntaInicial } = props;

  const [preguntaTxt, setPreguntaTxt] = useState(preguntaInicial.preguntaTxt);
  const [categorias, setCategorias] = useState<Categoria[] | null>(null);
  const [categoriaSeleccionadaId, setCategoriaSeleccionadaId] = useState(preguntaInicial.categoriaId);
  const [tiposPregunta, setTiposPregunta] = useState<TipoPregunta[] | null>(null);
  const [tipoPreguntaSeleccionadoId, setTipoPreguntaSeleccionadoId] = useState(preguntaInicial.tipoId);
  const [openSnackbarConfirm, setSnackbarConfirm] = useState(false);

  useEffect(() => {
    if (categorias) {
      return;
    }

    fetch(CATEGORIA_GET, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      }
    }).then((response) => {
      response.json().then((categorias: Categoria[]) => {
        setCategorias(categorias);
      });
    });
  }, [categorias]);

  useEffect(() => {
    if (tiposPregunta) {
      return;
    }

    fetch(TIPO_PREGUNTA_GET, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      }
    }).then((response) => {
      response.json().then((tiposPregunta: TipoPregunta[]) => {
        setTiposPregunta(tiposPregunta);
      });
    });
  }, [tiposPregunta]);

  const handleCancel = () => {
    onClose();
  };

  const handleOk = () => {
    onClose();
    const pregunta = {
      preguntaTxt: preguntaTxt, categoriaId: categoriaSeleccionadaId,
      tipoPreguntaId: tipoPreguntaSeleccionadoId
    } as Pregunta;
    updPregunta(props.preguntaInicial.id, pregunta);
    setSnackbarConfirm(true);
  };

  const handlePreguntaChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPreguntaTxt((event.target as HTMLInputElement).value);
  };

  const handleCategoriaChange = (event: React.MouseEvent<HTMLElement>, categoriaId: string) => {
    setCategoriaSeleccionadaId(categoriaId);
  };

  const handleTipoPreguntaChange = (event: React.MouseEvent<HTMLElement>, tipoPreguntaId: string) => {
    setTipoPreguntaSeleccionadoId(tipoPreguntaId);
  };

  function updPregunta(idPregunta: string, pregunta: Pregunta) {

    return fetch(PREGUNTA_ID_GET(idPregunta), {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`,
      },
      body: JSON.stringify({
        'preguntaTxt': pregunta.preguntaTxt,
        'categoriaId': pregunta.categoriaId,
        'tipoId': pregunta.tipoPreguntaId
      })
    })
  }

  function creaPregunta(pregunta: Pregunta) {
    const user = localStorage.getItem('user');
    let token = JSON.parse(user || '{}');
    console.log("token:" + token);

    return fetch(PREGUNTA_GET, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`,
      },
      body: JSON.stringify({
        'preguntaTxt': pregunta.preguntaTxt,
        'categoriaId': pregunta.categoriaId,
        'tipoId': pregunta.tipoPreguntaId
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
    <DialogTitle> Actualizar pregunta </DialogTitle>
    <DialogContent sx={{ p: 2 }}>
      <DialogContentText>
        Introduce los datos para modificar la pregunta.
      </DialogContentText>
      <TextField
        autoFocus
        margin="normal"
        label="Pregunta"
        type="text"
        fullWidth
        variant="standard"
        value={preguntaTxt}
        onChange={handlePreguntaChange}
      />
      <TextField
        select
        label="Categoría"
        margin="normal"
        fullWidth
        variant="standard"
        defaultValue={props.preguntaInicial.categoriaCat}
      >
        {categorias && categorias.map((categoria) => (
          <MenuItem
            key={categoria.id}
            value={categoria.categoriaCat}
            onClick={(event) => handleCategoriaChange(event, categoria.id)}>
            {categoria.categoriaCat}
          </MenuItem>
        ))}
      </TextField>
      <TextField
        select
        label="Tipo de pregunta"
        margin="normal"
        fullWidth
        variant="standard"
        defaultValue={props.preguntaInicial.tipo}
      >
        {tiposPregunta && tiposPregunta.map((tipoPregunta) => (
          <MenuItem
            key={tipoPregunta.id}
            value={tipoPregunta.tipo}
            onClick={(event) => handleTipoPreguntaChange(event, tipoPregunta.id)}>
            {tipoPregunta.tipo}
          </MenuItem>
        ))}
      </TextField>
    </DialogContent>
    <DialogActions>
      <Button onClick={handleCancel}> Cerrar </Button>
      <Button onClick={handleOk}> Actualizar </Button>
    </DialogActions>
  </Dialog>);
}

export default PreguntaUpdDialog;