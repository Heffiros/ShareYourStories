import { User } from '../../src/domain/user'

describe('User domain', () => {
  it('should hash password when registering', async () => {
    const user = await User.register({ email: 'a@b.com', password: 'secret' })
    expect(user.password).not.toBe('secret') // doit être hashé
  })
})
