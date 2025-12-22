import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      // This tells React: "If I ask for /api/ProcessShipment, send it to the Azure Function"
      '/api': {
        target: 'http://localhost:7017',
        changeOrigin: true,
        secure: false,
      }
    }
  }
})
