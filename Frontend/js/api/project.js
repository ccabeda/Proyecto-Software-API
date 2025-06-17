import { apiGet, apiPost, apiPut } from "./api.js";

export async function createProjectProposal(data) {
  return await apiPost("/Project", data);
}

export async function updateProjectProposal(id, updatedData) {
  return await apiPut(`/Project/${id}`, updatedData);
}

export async function saveApprovalDecision(proposalId, reviewData) {
  return await apiPut(`/Project/${proposalId}/decision`, reviewData);
}

export async function getFilteredProposals(filters = {}) {
  const params = new URLSearchParams();

  if (filters.title) params.append("title", filters.title);
  if (filters.status) params.append("status", filters.status);
  if (filters.applicant) params.append("applicant", filters.applicant); 
  if (filters.approvalUser) params.append("approvalUser", filters.approvalUser); 
  const url = `https://localhost:7298/api/Project?${params.toString()}`;
  const response = await fetch(url);
  return await response.json();
}

export async function getProjectProposalById(id) {
  try {
    return await apiGet(`/Project/${id}`);
  } catch (err) {
    if (err.message.includes("404")) return { notFound: true };
    throw new Error(err.message || "Error al obtener los datos de la propuesta");
  }
}