import axiosClient from "../api/axiosClient";

export const getProducts = async () => {
  const response = await axiosClient.get("/products");
  return response.data;
};