import "./App.css";
import { Route, Routes, Navigate } from "react-router-dom";
import Authorization from "./pages/authorization/Authorization.tsx";
import Login from "./pages/login/Login.tsx";
import Register from "./pages/register/Register.tsx";
import MainPage from "./pages/mainPage/MainPage.tsx";
import Game from "./pages/game/Game.tsx";

function App() {
  return (
    <>
      <Routes>
        <Route path="/" element={<Navigate to="/games" />} />
        <Route path="/auth" element={<Authorization />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/games" element={<MainPage />} />
        <Route path="/games/:id" element={<Game />} />
      </Routes>
    </>
  );
}

export default App;
