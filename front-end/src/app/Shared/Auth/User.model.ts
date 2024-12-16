export type User = {
    id: string;
    email: string;
    role: "admin" | "normal-owner";
    token?: string;
}