function MainLayout({ children }) {
  return (
    <div className="min-h-screen p-6">
      <nav className="mb-6 flex gap-4">
        <a href="/">Home</a>
        <a href="/login">Login</a>
        <a href="/register">Register</a>
      </nav>

      {children}
    </div>
  );
}

export default MainLayout;