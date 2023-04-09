import { Counter } from "./components/Counter";
import FeeTypes  from "./components/FeeTypes";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import Login from "./components/Login/Login";
import Event from "./components/Events";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
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

];

export default AppRoutes;
