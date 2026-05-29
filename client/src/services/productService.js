import axiosClient from "../api/axiosClient";

export const getProducts = async (
  page = 1,
  pageSize = 8,
  search = ""
) => {
  const response = await axiosClient.get(
    "/products",
    {
      params: {
        pageNumber: page,
        pageSize,
        search,
      },
    }
  );

  return response.data;
};

export const getProductById = async (id) => {
  const response = await axiosClient.get(
    `/products/${id}`
  );

  return response.data;
};

export const createProduct = async (data) => {
  const response = await axiosClient.post(
    "/products",
    data
  );

  return response.data;
};

export const deleteProduct = async (id) => {
  const response = await axiosClient.delete(
    `/products/${id}`
  );

  return response.data;
};

export const getCategories = async () => {
  const response = await axiosClient.get("/categories");
  return response.data;
};