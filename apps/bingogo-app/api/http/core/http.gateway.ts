import axios, { type AxiosInstance, type AxiosResponse } from 'axios';

export const serverErrorText: string =
  'Oops, Something bad happened, refresh the page and try later.';

export abstract class HttpGateway {
  protected axiosClient: AxiosInstance;

  protected abstract path: string;

  constructor() {
    this.axiosClient = axios.create({
      baseURL: `${process.env.API_ROUTE}api`,
      headers: {
        'Content-Type': 'application/json',
        // ...authHeader(),
      },
    });

    this.setupInterceptor();
  }

  private setupInterceptor(): void {
    this.axiosClient.interceptors.request.use((config: any) => {
      // TODO: Add NProgress
      return config;
    });

    this.axiosClient.interceptors.response.use(
      (res: AxiosResponse) => {
        // TODO: Add NProgress

        return Promise.resolve(res);
      },
      (err: any) => {
        // TODO: Add NProgress

        console.log(err);
        if (!err?.response) {
          // refresh();
        }

        const { status } = err.response;

        if (status === 400) {
          const data = (err.response?.data as any)?.detail;
          throw data;
        }

        if (status === 401) {
          // refresh();
        }

        if (status === 404) {
          const data = err.response?.data as any;
          throw data;
        }

        if (status === 500) {
          console.error(err);
          throw serverErrorText;
        }
      }
    );
  }
}
