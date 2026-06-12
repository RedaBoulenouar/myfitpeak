export function AboutPage() {
  return (
    <main className="page editorial">
      <p className="eyebrow">A propos</p>
      <h1>myfitpeak rend le coaching sportif plus accessible.</h1>
      <p>
        La plateforme aide les clients a trouver un coach adapte a leur niveau,
        leur discipline et leur budget. Les coachs peuvent presenter leur profil,
        gerer leurs disponibilites, publier des evenements et suivre leurs clients.
      </p>
      <div className="grid">
        <section className="card">
          <h2>Pour les clients</h2>
          <p>Recherche, avis, reservations, evenements et paiement simule.</p>
        </section>
        <section className="card">
          <h2>Pour les coachs</h2>
          <p>Profil professionnel, tarifs, rendez-vous, programmes et cours collectifs.</p>
        </section>
      </div>
    </main>
  );
}
