import fastify from '../../src/app' // ton instance Fastify
import request from 'supertest'

describe('Auth routes', () => {
  it('register route should create a new user', async () => {
    const res = await request(fastify.server)
      .post('/register')
      .send({ email: 'test@domain.com', password: '123456' })
    expect(res.status).toBe(200)
    expect(res.body.email).toBe('test@domain.com')
    expect(res.body.id).toBeDefined()
  })
})
