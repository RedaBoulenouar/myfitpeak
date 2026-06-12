export function ContactPage() {
  return (
    <main className="page contact-layout">
      <section>
        <p className="eyebrow">Nous contacter</p>
        <h1>Une question sur My Fitt Peak ?</h1>
        <p>
          Envoyez votre message a l'equipe pour parler partenariat coach,
          support client ou lancement de la plateforme.
        </p>
      </section>
      <form className="contact-form">
        <input placeholder="Nom complet" />
        <input type="email" placeholder="Email" />
        <select defaultValue="client">
          <option value="client">Je suis client</option>
          <option value="coach">Je suis coach</option>
          <option value="partner">Partenariat</option>
        </select>
        <textarea placeholder="Votre message" rows={6} />
        <button type="button">Envoyer</button>
      </form>
    </main>
  );
}
