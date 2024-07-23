async function getCardATR() {
    try {
        let response = await fetch('http://localhost:5000/reader/');
        if (response.ok) {
            let atr = await response.text();
            return atr;
        } else {
            console.error('Error fetching ATR:', response.status);
            return "Error: Problema de comunicación con el servicio de conexión al lector de tarjetas";
        }
    } catch (error) {
        console.error('Error:', error);
        return "Error: El servicio de conexión al lector de tarjetas no está disponible";
    }
}