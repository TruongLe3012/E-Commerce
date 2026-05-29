import axiosClient from "../api/axiosClient";

export const createOrder = async (data) => {
  const response = await axiosClient.post(
    "/orders",
    data
  );

  return response.data;
};