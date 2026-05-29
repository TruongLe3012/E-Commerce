import { Routes, Route } from "react-router-dom";

import MainLayout from "../layouts/MainLayout";

import HomePage from "../pages/HomePage";
import LoginPage from "../pages/LoginPage";
import RegisterPage from "../pages/RegisterPage";
import ProfilePage from "../pages/ProfilePage";
import ProductsPage from "../pages/ProductsPage";

import ProtectedRoute from "./ProtectedRoute";

function AppRoutes() {
  return (
    <MainLayout>
      <Routes>
        <Route path="/" element={<HomePage />} />

        <Route path="/login" element={<LoginPage />} />

        <Route
          path="/profile"
          element={
            <ProtectedRoute>
              <ProfilePage />
            </ProtectedRoute>
          }
          
        />
        <Route path="/products" element={<ProductsPage />} />
        

        <Route
          path="/register"
          element={<RegisterPage />}
        />
      </Routes>
    </MainLayout>
  );
}

export default AppRoutes;