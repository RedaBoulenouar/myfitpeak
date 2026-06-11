export function CoachDashboardPage() {
  return (
    <main className="page">
      <h1>Tableau de bord coach</h1>
      <div className="grid">
        <section className="card">
          <h2>Profil</h2>
          <p>Bio, sports, tarifs et disponibilites.</p>
        </section>
        <section className="card">
          <h2>Evenements</h2>
          <p>Creation de cours collectifs gratuits ou payants.</p>
        </section>
        <section className="card">
          <h2>Programmes</h2>
          <p>Partage et suivi des programmes sportifs clients.</p>
        </section>
      </div>
    </main>
  );
}
