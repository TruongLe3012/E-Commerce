import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";

import "./index.css";
import AppRoutes from "./routes/AppRoutes";
import CartProvider from "./context/CartContext";

import { Toaster } from "react-hot-toast";

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <CartProvider>
      <BrowserRouter>
        <AppRoutes />

        <Toaster position="top-right" />
      </BrowserRouter>
    </CartProvider>
  </React.StrictMode>
);