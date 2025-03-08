import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  /* config options here */
  output: "export",

  // Rewrites for local development only, will not be exported to a static build
  // Warnings are annoying
  // Handled by cloudfront in environments
  // https://nextjs.org/docs/app/api-reference/config/next-config-js/rewrites
  async rewrites() {
    return [
      {
        source: "/api/:path*",
        destination: "http://localhost:61386/:path*", // Update if different running the API locally
      },
    ];
  },
};

export default nextConfig;
