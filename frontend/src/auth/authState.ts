const tokenStorageKey = "myfitpeak.jwt";

type JwtPayload = {
  role?: string;
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"?: string;
  exp?: number;
};

export type AuthState = {
  isAuthenticated: boolean;
  role: string | null;
};

export function getAuthState(): AuthState {
  const token = localStorage.getItem(tokenStorageKey);

  if (!token) {
    return { isAuthenticated: false, role: null };
  }

  const payload = parseJwtPayload(token);

  if (payload?.exp && payload.exp * 1000 <= Date.now()) {
    localStorage.removeItem(tokenStorageKey);
    return { isAuthenticated: false, role: null };
  }

  return {
    isAuthenticated: true,
    role: payload?.role ?? payload?.["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ?? null
  };
}

function parseJwtPayload(token: string): JwtPayload | null {
  const [, payload] = token.split(".");

  if (!payload) {
    return null;
  }

  try {
    return JSON.parse(atob(payload.replace(/-/g, "+").replace(/_/g, "/"))) as JwtPayload;
  } catch {
    return null;
  }
}
