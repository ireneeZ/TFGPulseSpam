import { Box, Button, Checkbox, Container, FormControl, FormControlLabel, FormGroup, FormLabel, Typography } from "@mui/material";
import { useState, useEffect } from "react";
import { PREGUNTA_GET } from "../../constants/urls";
import { Pregunta } from "../../types/Pregunta";
import { token } from "../../constants/authInfo";

let preguntasCheckedExport = new Array<string>();

type CheckboxPreguntasProps = {
  preguntasChecked: string[];
};

function CheckboxPreguntas(props: CheckboxPreguntasProps) {

  const [preguntas, setPreguntas] = useState<Pregunta[]>();
  const [preguntasChecked, setPreguntasChecked] = useState<string[]>(props.preguntasChecked);

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { id, checked } = event.target;

    console.log(`${id} está ${checked}`);

    if (checked) {
      preguntasChecked.push(id);
    }

    else {
      var index = preguntasChecked.indexOf(event.target.id);
      preguntasChecked.splice(index, 1);
    }

    console.log(`Mis preguntas son: `);
    preguntasChecked.forEach(element => {
      console.log(`${element}`);
    });

    preguntasCheckedExport = preguntasChecked;
  };

  useEffect(() => {
    fetch(PREGUNTA_GET, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`,
      }
    })
      .then((data) => data.json())
      .then((data) => setPreguntas(data))

  }, [])

  return (
    <FormControl sx={{ m: 3 }} component="fieldset" variant="standard">
      <FormLabel component="legend">Preguntas</FormLabel>
      <FormGroup>
        {preguntas && preguntas.map((pregunta: Pregunta) =>
          <FormControlLabel
            control={
              <Checkbox onChange={handleChange} id={pregunta.id}/>
            }
            label={pregunta.preguntaTxt} id={pregunta.id} />)}
      </FormGroup>
    </FormControl>
  );
}

export default CheckboxPreguntas;
export {preguntasCheckedExport}