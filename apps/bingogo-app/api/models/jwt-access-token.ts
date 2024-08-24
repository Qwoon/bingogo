export const TOKEN: string = 'JWT';
export const REFRESH_TOKEN: string = 'REFRESH_TOKEN';

export interface JwtAccessToken {
  token: string;
  refreshToken: string;
  roles: string[];
}
