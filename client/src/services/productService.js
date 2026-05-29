import axiosClient from "../api/axiosClient";

export const getProducts = async (
  page = 1,
  pageSize = 8,
  search = ""
) => {
  const response = await axiosClient.get(
    `/products?page=${page}&pageSize=${pageSize}&search=${search}`
  );

  return response.data;
};