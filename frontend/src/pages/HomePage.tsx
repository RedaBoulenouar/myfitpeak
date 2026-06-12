import { Link } from "react-router-dom";

const strengths = [
  "Coachs verifies et profils detailles",
  "Reservations en ligne ou en presentiel",
  "Evenements sportifs gratuits ou payants"
];

export function HomePage() {
  return (
    <main>
      <section className="hero">
        <div className="hero-content">
          <p className="eyebrow">Plateforme coachs & clients</p>
          <h1>Atteignez votre meilleur niveau avec le bon accompagnement.</h1>
          <p>
            myfitpeak connecte les clients avec des coachs sportifs, des cours
            collectifs, des programmes personnalises et un suivi clair.
          </p>
          <div className="hero-actions">
            <Link className="primary-link" to="/register">Creer un compte</Link>
            <Link className="secondary-link" to="/coaches">Trouver un coach</Link>
          </div>
        </div>
        <div className="hero-panel">
          <span className="panel-label">Cette semaine</span>
          <strong>24 coachs disponibles</strong>
          <p>Fitness, boxe, yoga, running, nutrition sportive.</p>
        </div>
      </section>

      <section className="section">
        <div className="section-heading">
          <p className="eyebrow">Pourquoi myfitpeak</p>
          <h2>Un parcours simple avant, pendant et apres la seance.</h2>
        </div>
        <div className="grid">
          {strengths.map((strength) => (
            <article className="card" key={strength}>
              <h3>{strength}</h3>
              <p>
                Une experience pensee pour reserver vite, suivre ses objectifs
                et garder une relation claire avec son coach.
              </p>
            </article>
          ))}
        </div>
      </section>
    </main>
  );
}
