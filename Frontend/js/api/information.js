import { apiGet } from "./api.js"; // ruta relativa según estructura

export function getAreas() {
  return apiGet("/Area");
}

export function getProjectTypes() {
  return apiGet("/ProjectType");
}

export function getStatuses() {
  return apiGet("/ApprovalStatus");
}

export function getUsers() {
  return apiGet("/User");
}