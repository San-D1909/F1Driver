// CypressTest.js created with Cypress
//
// Start writing your Cypress tests below!
// If you're unfamiliar with how Cypress works,
// check out the link below and learn how to write your first test:
// https://on.cypress.io/writing-first-test

describe('Front-End Test', () => {
    /* ==== Test Created with Cypress Studio ==== */
    it('Login', function() {
        /* ==== Generated with Cypress Studio ==== */
        cy.visit('http://localhost:5002/Login');
        cy.get(':nth-child(1) > .form-control').clear();
        cy.get(':nth-child(1) > .form-control').type('Cypress@boy');
        cy.get(':nth-child(2) > .form-control').click();
        cy.get(':nth-child(2) > .form-control').type('123@#$Test');
        cy.get('.btn').click();
        cy.get('iframe', { timeout: 10000 }).should('be.visible');
    });


    /* ==== Test Created with Cypress Studio ==== */
    it('Test Pages', function() {
        /* ==== Generated with Cypress Studio ==== */
        cy.visit('http://localhost:5002/UpcomingRace');
        cy.get('div.card', { timeout: 10000 }).should('be.visible');
        cy.visit('http://localhost:5002/DriverStandings');
        cy.get('table', { timeout: 10000 }).should('be.visible');
        cy.visit('http://localhost:5002/ConstructorStandings');
        cy.get('table', { timeout: 10000 }).should('be.visible');
        cy.visit('http://localhost:5002/RaceCalendar');
        cy.get('body', { timeout: 10000 }).should('be.visible');
        /* ==== End Cypress Studio ==== */
    });
    /* ==== Test Created with Cypress Studio ==== */
    it('Test group', function() {
        /* ==== Generated with Cypress Studio ==== */
        cy.visit('http://localhost:5002/login');
        cy.get(':nth-child(1) > .form-control').clear();
        cy.get(':nth-child(1) > .form-control').type('Cypress@boy');
        cy.get(':nth-child(2) > .form-control').clear();
        cy.get(':nth-child(2) > .form-control').type('123@#$Test');
        cy.get('.btn').click();
        cy.get('iframe', { timeout: 10000 }).should('be.visible');
        cy.visit('http://localhost:5002/Group');
        cy.get('table', { timeout: 10000 }).should('be.visible');
        cy.get('.table > :nth-child(1) > :nth-child(2)', {timeout:1000}).click();
        cy.get('table', { timeout: 10000 }).should('be.visible');
        cy.get('.card-body > :nth-child(1)').click();
        cy.get('input', { timeout: 10000 }).should('be.visible'); 
        cy.get('input').clear();
        cy.get('input', {timeout:1000}).type('cypress@boy');
        cy.get(':nth-child(2) > .btn', { timeout: 1000 }).click();
        cy.get('.card-body > :nth-child(1) > :nth-child(1)').click();
        cy.get('table', { timeout: 10000 }).should('be.visible');
        cy.get('.card-body > :nth-child(3)', { timeout: 1000 }).click();
        cy.get('table', { timeout: 10000 }).should('be.visible');
        cy.visit('http://localhost:5002/logout');
        /* ==== End Cypress Studio ==== */
    });
})