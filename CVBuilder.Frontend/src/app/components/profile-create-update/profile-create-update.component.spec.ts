import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileCreateUpdateComponent } from './profile-create-update.component';

describe('ProfileCreateUpdateComponent', () => {
  let component: ProfileCreateUpdateComponent;
  let fixture: ComponentFixture<ProfileCreateUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProfileCreateUpdateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileCreateUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
