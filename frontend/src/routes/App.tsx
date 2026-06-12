import { Link, Route, Routes } from "react-router-dom";
import { getAuthState } from "../auth/authState";
import { AboutPage } from "../pages/AboutPage";
import { CoachSearchPage } from "../pages/CoachSearchPage";
import { CoachDashboardPage } from "../pages/CoachDashboardPage";
import { ContactPage } from "../pages/ContactPage";
import { HomePage } from "../pages/HomePage";
import { LoginPage } from "../pages/LoginPage";
import { RegisterPage } from "../pages/RegisterPage";

export function App() {
  const auth = getAuthState();
  const isCoach = auth.role === "Coach";

  return (
    <div className="app-shell">
      <header className="topbar">
        <Link to="/" className="brand">My Fitt Peak</Link>
        <nav>
          <Link to="/">Accueil</Link>
          <Link to="/about">A propos</Link>
          <Link to="/contact">Contact</Link>
          {auth.isAuthenticated ? (
            <>
              <Link to="/coaches">Coachs</Link>
              {isCoach && <Link to="/coach">Dashboard coach</Link>}
              <button
                className="link-button"
                type="button"
                onClick={() => {
                  localStorage.removeItem("myfitpeak.jwt");
                  window.location.href = "/";
                }}
              >
                Deconnexion
              </button>
            </>
          ) : (
            <>
              <Link to="/login">Connexion</Link>
              <Link to="/register" className="nav-cta">Inscription</Link>
            </>
          )}
        </nav>
      </header>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/about" element={<AboutPage />} />
        <Route path="/contact" element={<ContactPage />} />
        <Route path="/coaches" element={<CoachSearchPage />} />
        <Route path="/coach" element={<CoachDashboardPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
      </Routes>
    </div>
  );
}
