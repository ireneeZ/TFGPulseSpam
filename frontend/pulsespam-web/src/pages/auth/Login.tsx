import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { UsuarioLogin } from '../../types/UsuarioLogin';
import { LOGIN } from '../../constants/urls';
import { Usuario } from '../../types/Usuario';
import { Navigate, useNavigate } from "react-router-dom";
import { useState } from 'react';
import { Alert } from '@mui/material';

function Login() {
    const navigate = useNavigate();

    const [email, setEmail] = React.useState<string>();
    const [password, setPassword] = React.useState<string>();

    const alert = {
        showFailAlert: false
    }

    const [state, setState] = useState(alert);
    const [alertText, setAlertText] = React.useState<string>();

    const handleEmailChange = (event: React.ChangeEvent<HTMLElement>) => {
        setEmail((event.target as HTMLInputElement).value);
    };

    const handlePasswordChange = (event: React.ChangeEvent<HTMLElement>) => {
        setPassword((event.target as HTMLInputElement).value);
    };

    function handleLogin() {
        if (email == null || password == null) {
            setAlertText("Es necesario completar todos los campos");
            setState({
                showFailAlert: true
            })
            return null;
        }

        const regex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;
        if (email && email.match(regex)) {
            const user = {
                Email: email,
                Password: password,
                IsAdmin: true
            } as UsuarioLogin;
            login(user);
        } else {
            setAlertText("Formato incorrecto de email");
            setState({
                showFailAlert: true
            })
            return null;
        }
    }

    function login(usuario: UsuarioLogin) {
        return fetch(LOGIN, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                'Email': usuario.Email,
                'Password': usuario.Password,
                'IsAdmin': usuario.IsAdmin
            })
        })


            .then(response => response.json())
            .then(response => {
                if (!response.success) {
                    console.log(response);
                    throw new Error();
                }

                let token = response.accessToken;
                localStorage.setItem("user", JSON.stringify(token));
                navigate("/inicio");
            })
            .catch(err => {
                setState({
                    showFailAlert: true
                })
                if (err.message.includes("incorrecto")) {
                    setAlertText("Email o contraseña incorrectos");
                } else if (err.message.includes("administrador")) {
                    setAlertText("Permisos insuficientes. Contacta con un administrador");
                } else {
                    setAlertText("Se ha producido un error");
                }
            })

    }

    return (
        <Container maxWidth="xs">
            <Box
                sx={{
                    marginTop: 8,
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                }}

            >
                <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                    <LockOutlinedIcon />
                </Avatar>
                <Typography component="h1" variant="h5">
                    Iniciar sesión
                </Typography>
                <Box sx={{ mt: 1 }}>
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="email"
                        label="Email"
                        type="email"
                        name="email"
                        autoFocus
                        value={email}
                        onChange={handleEmailChange}
                    />
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        name="password"
                        label="Contraseña"
                        type="password"
                        id="password"
                        autoComplete="current-password"
                        value={password}
                        onChange={handlePasswordChange}
                    />
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                        onClick={handleLogin}
                    >
                        Iniciar sesión
                    </Button>
                </Box>
            </Box>
            {state.showFailAlert && <Alert severity={"error"}>{alertText}</Alert>}
        </Container>
    )
}

export default Login;