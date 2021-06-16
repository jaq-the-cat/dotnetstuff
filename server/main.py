import asyncio
from websockets import server

async def echo(socket: server.WebSocketServerProtocol, _):
    print("Connected")
    async for message in socket:
        if (not socket.open or socket.closed):
            return
        print(f">>> {message}\n")

if __name__ == '__main__':
    print("Starting server")
    asyncio.get_event_loop().run_until_complete(server.serve(echo, '0.0.0.0', 8000))
    asyncio.get_event_loop().run_forever()
