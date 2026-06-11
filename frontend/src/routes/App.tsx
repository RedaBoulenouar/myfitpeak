import { Link, Route, Routes } from "react-router-dom";
import { CoachSearchPage } from "../pages/CoachSearchPage";
import { CoachDashboardPage } from "../pages/CoachDashboardPage";
import { LoginPage } from "../pages/LoginPage";

export function App() {
  return (
    <div className="app-shell">
      <header className="topbar">
        <Link to="/" className="brand">My Fitt Peak</Link>
        <nav>
          <Link to="/">Coachs</Link>
          <Link to="/coach">Dashboard coach</Link>
          <Link to="/login">Connexion</Link>
        </nav>
      </header>
      <Routes>
        <Route path="/" element={<CoachSearchPage />} />
        <Route path="/coach" element={<CoachDashboardPage />} />
        <Route path="/login" element={<LoginPage />} />
      </Routes>
    </div>
  );
}
