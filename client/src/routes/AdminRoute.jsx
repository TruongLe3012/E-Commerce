import { Navigate } from "react-router-dom";

import { getUser } from "../utils/auth";

function AdminRoute({ children }) {
  const user = getUser();

  if (!user) {
    return <Navigate to="/login" />;
  }

  const role =
    user.role ||
    user[
      "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    ];

  if (role !== "Admin") {
    return <Navigate to="/" />;
  }

  return children;
}

export default AdminRoute;