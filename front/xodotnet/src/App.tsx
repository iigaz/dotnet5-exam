import "./App.css";
import { Route, Routes } from "react-router-dom";
import Authorization from "./pages/authorization/Authorization.tsx";
import Login from "./pages/login/Login.tsx";
import Register from "./pages/register/Register.tsx";

function App() {
  return (
    <>
      <Routes>
        <Route path="/auth" element={<Authorization />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
      </Routes>
    </>
  );
}

export default App;
