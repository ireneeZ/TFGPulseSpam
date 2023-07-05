import { Box, Button, Card, CardMedia, CircularProgress, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, MenuItem, TextField } from "@mui/material";
import { useEffect, useState } from "react";
import { Categoria } from "../../types/Categoria";
import { CATEGORIA_GET, PREGUNTA_GET, TIPO_PREGUNTA_GET } from "../../constants/urls";
import { Pregunta } from "../../types/Pregunta";
import { TipoPregunta } from "../../types/TipoPregunta";
import { DataGrid, GridEventListener } from "@mui/x-data-grid";
import { PreguntaTransfer } from "../../types/PreguntaTransfer";
import { token } from "../../constants/authInfo";

type PreguntaDialogProps = {
  isOpen: boolean;
  preguntaTxt: string;
  categoriaCat: string;
  tipo: string;
  onClose: (value?: string) => void;
};

function PreguntaDialog(props: PreguntaDialogProps) {
  const { isOpen, onClose, preguntaTxt: preguntaProp, categoriaCat: categoriaProp, tipo: tipoProp } = props;

  const [preguntaTxt, setPreguntaTxt] = useState(preguntaProp);
  const [categorias, setCategorias] = useState<Categoria[] | null>(null);
  const [categoriaSeleccionada, setCategoriaSeleccionada] = useState(categoriaProp);
  const [tiposPregunta, setTiposPregunta] = useState<TipoPregunta[] | null>(null);
  const [tipoPreguntaSeleccionado, setTipoPreguntaSeleccionado] = useState(tipoProp);

  const [errorPreguntaTxt, setErrorPreguntaTxt] = useState(false);

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
    setPreguntaTxt("");
  };

  const handleOk = () => {
    if (preguntaTxt != "") {
      onClose();
      const pregunta = {
        preguntaTxt: preguntaTxt, categoriaId: categoriaSeleccionada,
        tipoPreguntaId: tipoPreguntaSeleccionado
      } as Pregunta;
      creaPregunta(pregunta);
      setPreguntaTxt("");
    } else {
      setErrorPreguntaTxt(true);
    }
  };

  const handlePreguntaChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPreguntaTxt((event.target as HTMLInputElement).value);
    if (errorPreguntaTxt) {
      setErrorPreguntaTxt(false);
    }
  };

  const handleCategoriaChange = (event: React.MouseEvent<HTMLElement>, categoriaId: string) => {
    setCategoriaSeleccionada(categoriaId);
  };

  const handleTipoPreguntaChange = (event: React.MouseEvent<HTMLElement>, tipoPreguntaId: string) => {
    setTipoPreguntaSeleccionado(tipoPreguntaId);
  };

  function creaPregunta(pregunta: Pregunta) {
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
    <DialogTitle> Crear pregunta </DialogTitle>
    <DialogContent sx={{ p: 2 }}>
      <DialogContentText>
        Introduce los datos para crear una nueva pregunta.
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
        defaultValue=""
        helperText={errorPreguntaTxt ? "Es necesario introducir una pregunta" : ""}
        error={errorPreguntaTxt}
      />
      <TextField
        select
        label="Categoría"
        margin="normal"
        fullWidth
        variant="standard"
        defaultValue=""
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
      <Button onClick={handleOk}> Crear </Button>
    </DialogActions>
  </Dialog>);
}

export default PreguntaDialog;