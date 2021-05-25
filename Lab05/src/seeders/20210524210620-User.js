'use strict';

module.exports = {
  up: async (queryInterface, Sequelize) => {
    /**
     * Add seed commands here.
     *
     * Example:
     * await queryInterface.bulkInsert('People', [{
     *   name: 'John Doe',
     *   isBetaMember: false
     * }], {});
    */

    await queryInterface.bulkInsert('Users', [{
      email: 'grzegorzbrzeczyszczykiewicz@poczta.pl',
      phone: '+48888888888',
      password: "$argon2i$v=19$m=4096,t=3,p=1$EUAA/Xh5UXEyl/fB5i1xjQ$+FzjgvDlJ2nUMuUUyNrzmuvMafsxnGUIL920zxe9hiY",
      salt: "4dc5d4b6592cffb36ca8b2482a5f05d3a6974943e91653fc3246ba270080e67e",
      fullName: "Grzegorz Brzęczyszczykiewicz",
      userRoleId: 2,
      createdAt: new Date(),
      updatedAt: new Date()
    }])

  },

  down: async (queryInterface, Sequelize) => {
    /**
     * Add commands to revert seed here.
     *
     * Example:
     * await queryInterface.bulkDelete('People', null, {});
     */
    await queryInterface.bulkDelete('Users', null, {})
  }
};
