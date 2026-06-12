export function RegisterPage() {
  return (
    <main className="page auth-page">
      <form className="auth-form">
        <h1>Creer un compte</h1>
        <input placeholder="Prenom" />
        <input placeholder="Nom" />
        <input type="email" placeholder="Email" />
        <input type="password" placeholder="Mot de passe" />
        <select defaultValue="Client">
          <option value="Client">Client</option>
          <option value="Coach">Coach</option>
        </select>
        <button type="button">S'inscrire</button>
      </form>
    </main>
  );
}
