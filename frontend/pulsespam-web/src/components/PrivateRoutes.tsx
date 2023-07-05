import { Navigate, Outlet } from 'react-router-dom'

function PrivateRoutes() {
  let auth = hasJWT();

    if (auth) {
      return (<Outlet/>)
    } else {
      return (<Navigate to='/login'/>)
    }
}

function hasJWT() {
  let hasJwt = false;

  if (localStorage.getItem("user") != null) {
      hasJwt = true;
  }
 
  return hasJwt;
}

export default PrivateRoutes;