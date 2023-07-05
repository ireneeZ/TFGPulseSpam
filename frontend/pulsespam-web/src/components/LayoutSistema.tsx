import { Outlet } from 'react-router-dom';
import Sidebar from './Sidebar';
import Appbar from './Appbar';
import { Grid } from '@mui/material';

const LayoutSistema = () => (
  <Grid container columns={{ md: 10 }}>
    <Grid item xs={2}>
      <Sidebar />
    </Grid>
    <Grid item xs={8}>
      <Appbar />
      <Outlet />
    </Grid>
  </Grid>
);

export default LayoutSistema;