
import FeeTypes  from "./components/FeeTypes";
import { Home } from "./components/Home";
import Login from "./components/Login/Login";
import Event from "./components/Events";
import Persons from "./components/Persons";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  
   {
       path: '/feetypes',
       element: <FeeTypes />
    },
    {
        path: '/login',
        element: <Login />
    },
    {
        path: '/events',
        element: <Event />
    },
    {
        path: '/guests',
        element: <Persons />
    },

];

export default AppRoutes;
