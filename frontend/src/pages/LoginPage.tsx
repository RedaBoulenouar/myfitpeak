export function LoginPage() {
  return (
    <main className="page auth-page">
      <form className="auth-form">
        <h1>Connexion</h1>
        <input type="email" placeholder="Email" />
        <input type="password" placeholder="Mot de passe" />
        <button type="button">Se connecter</button>
      </form>
    </main>
  );
}
