function MainLayout({ children }) {
  const token = localStorage.getItem("token");

  const handleLogout = () => {
    localStorage.removeItem("token");

    window.location.href = "/login";
  };

  return (
    <div className="min-h-screen p-6">
      <nav className="mb-6 flex gap-4">
        <a href="/">Home</a>

        {!token && (
          <>
            <a href="/login">Login</a>
            <a href="/register">Register</a>
          </>
        )}

        {token && (
          <>
            <a href="/profile">Profile</a>
            <a href="/products">Products</a>

            <button onClick={handleLogout}>
              Logout
            </button>
          </>
        )}
      </nav>

      {children}
    </div>
  );
}

export default MainLayout;