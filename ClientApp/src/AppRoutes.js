
import FeeTypes  from "./components/FeeTypes";
import Event from "./components/Events";
import Persons from "./components/Persons";
import Companies from "./components/Companies";
import Visitors from "./components/Visitors";

const AppRoutes = [
  {
    index: true,
    element: <Event />
  },
  
   {
       path: '/feetypes',
       element: <FeeTypes />
    },
    
    {
        path: '/events',
        element: <Event />
    },
    {
        path: '/guests',
        element: <Persons />
    },
    {
        path: '/companies',
        element: <Companies />
    },
    {
        path: '/visitors',
        element: <Visitors />
    },

];

export default AppRoutes;
